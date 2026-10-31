from flask import Flask, jsonify, request
import pyodbc

app = Flask(__name__)

CONN_STR = (
    "DRIVER={ODBC Driver 17 for SQL Server};"
    "SERVER=localhost;"
    "DATABASE=QuanLyPhongVe;"
    "Trusted_Connection=yes;"
)

@app.route("/api/movies", methods=["GET"])
def get_movie():
    conn = None
    try:
        conn = pyodbc.connect(CONN_STR)
        cursor = conn.cursor()
        cursor.execute("SELECT MaPhim, TenPhim, GiaGoc FROM PHIM")
        rows = cursor.fetchall()
        movies = []
        for r in rows:
            movies.append({
                "MaPhim": r[0],
                "TenPhim": r[1],
                "GiaGoc": float(r[2]) if r[2] else 0.0
            })
        return jsonify(movies), 200
    except Exception as e:
        print("LỖI SQL:", str(e))
        return jsonify({"err": str(e)}), 400
    finally:
        if conn:
            conn.close()


@app.route("/api/room", methods=["GET"])
def get_room():
    ma_phim = request.args.get("MaPhim")  # Lấy MaPhim từ query string
    if not ma_phim:
        return jsonify({"err": "Thiếu tham số MaPhim trên URL!"}), 400

    conn = None
    try:
        conn = pyodbc.connect(CONN_STR)
        cursor = conn.cursor()
        cursor.execute(
            "SELECT MaPhong, TenPhong, MaPhim FROM PHONG WHERE MaPhim = ?", (ma_phim,)
        )
        rows = cursor.fetchall()
        rooms = []
        for i in rows:
            # Đã đổi key sang không dấu khớp với PhongDto trong C#
            rooms.append({
                "MaPhong": i[0],
                "TenPhong": i[1],
                "MaPhim": i[2]
            })
        return jsonify(rooms), 200
    except Exception as e:
        return jsonify({"err": str(e)}), 400
    finally:
        if conn:
            conn.close()


@app.route("/api/Ghe", methods=["GET"])
def get_ghe():
    ma_phong = request.args.get("MaPhong")  # Lấy MaPhong từ query string
    if not ma_phong:
        return jsonify({"err": "Thiếu tham số MaPhong trên URL!"}), 400

    conn = None
    try:
        conn = pyodbc.connect(CONN_STR)
        cursor = conn.cursor()
        query = (
            "SELECT G.MaGhe, G.TenGhe, G.LoaiGhe, G.TTGhe, P.GiaGoc "
            "FROM GHE G "
            "JOIN PHONG PH ON PH.MaPhong = G.MaPhong "
            "JOIN PHIM P ON PH.MaPhim = P.MaPhim "
            "WHERE G.MaPhong = ?"
        )
        cursor.execute(query, (ma_phong,))
        rows = cursor.fetchall()
        ghes = []
        for i in rows:
            # Đã đổi key sang không dấu khớp chính xác với GheDto trong C#
            ghes.append({
                "MaGhe": i[0],
                "TenGhe": i[1],
                "LoaiGhe": i[2],
                "TTGhe": i[3],
                "GiaGoc": float(i[4]) if i[4] else 0.0,
            })
        return jsonify(ghes), 200
    except Exception as e:
        return jsonify({"err": str(e)}), 400
    finally:
        if conn:
            conn.close()


@app.route("/api/book", methods=["POST"])
def book():
    data = request.json
    if not data or "MaGheList" not in data:
        return (
            jsonify({"success": False, "message": "Danh sách ghế không hợp lệ!"}),
            400,
        )

    ma_ghe_list = data["MaGheList"]
    conn = None
    try:
        conn = pyodbc.connect(CONN_STR, autocommit=False)
        cursor = conn.cursor()

        total_price = 0

        for ma_ghe in ma_ghe_list:
            cursor.execute(
                "SELECT TTGhe FROM GHE WITH (UPDLOCK) WHERE Maghe = ?", (ma_ghe,)
            )
            row = cursor.fetchone()

            if not row:
                conn.rollback()
                return (
                    jsonify({
                        "success": False,
                        "message": f"Ghế mã {ma_ghe} không tồn tại!",
                    }),
                    404,
                )

            if row[0] == 1:
                conn.rollback()
                return (
                    jsonify({
                        "success": False,
                        "message": f"Ghế mã {ma_ghe} vừa có quầy khác đặt mất! Vui lòng chọn ghế khác.",
                    }),
                    400,
                )

            cursor.execute(
                """
                    SELECT p.GiaGoc * CASE g.LoaiGhe 
                        WHEN 2 THEN 1.5 WHEN 3 THEN 0.8 ELSE 1.0 END AS GiaVe
                    FROM GHE g
                    JOIN PHONG ph ON g.MaPhong = ph.MaPhong
                    JOIN PHIM p ON ph.MaPhim = p.MaPhim
                    WHERE g.Maghe = ?
                """,
                (ma_ghe,),
            )
            price_row = cursor.fetchone()
            if price_row:
                total_price += float(price_row[0])

        placeholders = ",".join(["?"] * len(ma_ghe_list))
        cursor.execute(
            f"UPDATE GHE SET TTGhe = 1 WHERE Maghe IN ({placeholders})",
            tuple(ma_ghe_list),
        )

        conn.commit()

        return (
            jsonify({
                "success": True,
                "message": "Đặt vé thành công!",
                "TongTien": total_price,
            }),
            200,
        )

    except Exception as e:
        if conn:
            conn.rollback()
        return jsonify({"success": False, "message": str(e)}), 500
    finally:
        if conn:
            conn.close()


if __name__ == "__main__":
    app.run(debug=True)
