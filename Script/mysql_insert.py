import pymysql
def data_append(conn,values):
    sql = "INSERT INTO inventory.asset(CPU,RAM,DISK,VideoController,NetworkAdapter,SoundCard,Monitor,SN,Brand,OS,LastCheckin) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')".format(values[0],values[1],values[2],values[3],values[4],values[5],values[6],values[7],values[8],values[9],values[10])
    try:
        with conn.cursor() as cursor:
            cursor.execute(sql)
        conn.commit()
        print("insert data finished.")
    except Exception as e:
        print(e.__str__())
        conn.rollback()
