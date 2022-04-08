import struct

filename = 'combined_sort_timestamp.csv'

lines=[]
with open(filename) as file:
    lines = file.read().splitlines()
lines = lines[1:]

data=[]
for line in lines:
    sline = line.split(',')
    # tuple (time,x,y,col)
    data.append( (\
        int(sline[0]) , \
        int(sline[2]) , \
        int(sline[3]) , \
        int(sline[4]) ) )

data.sort(key=lambda x: x[0])

with open("data.bin", "wb") as f:
    for d in data:
        f.write(d[1].to_bytes(2,byteorder='little'))
        f.write(d[1].to_bytes(2,byteorder='little'))
        f.write(d[2].to_bytes(2,byteorder='little'))
        f.write(d[3].to_bytes(1,byteorder='little'))