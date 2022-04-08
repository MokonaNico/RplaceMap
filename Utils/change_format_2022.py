import struct
import datetime
import time

filename = 'tiles_2022.csv'

colors=["#000000","#00756F","#009EAA","#00A368","#00CC78","#00CCC0",
        "#2450A4","#3690EA","#493AC1","#515252","#51E9F4","#6A5CFF",
        "#6D001A","#6D482F","#7EED56","#811E9F","#898D90","#94B3FF",
        "#9C6926","#B44AC0","#BE0039","#D4D7D9","#DE107F","#E4ABFF",
        "#FF3881","#FF4500","#FF99AA","#FFA800","#FFB470","#FFD635",
        "#FFF8B8","#FFFFFF"]

with open(filename) as file:
    with open("data.bin", "wb") as f:
        line = file.readline()
        line = file.readline()
        while line:
            sline = line.split(',')
            try:
                element = datetime.datetime.strptime(sline[0],"%Y-%m-%d %H:%M:%S.%f UTC")
            except ValueError:
                element = datetime.datetime.strptime(sline[0],"%Y-%m-%d %H:%M:%S UTC")
            timestamp = round(datetime.datetime.timestamp(element) * 1000)
            x = int(sline[3][1:])
            y = int(sline[4][:-2])
            col = colors.index(sline[2])
            f.write(timestamp.to_bytes(8,byteorder='little'))
            f.write(x.to_bytes(2,byteorder='little'))
            f.write(y.to_bytes(2,byteorder='little'))
            f.write(col.to_bytes(1,byteorder='little'))
            line = file.readline()

