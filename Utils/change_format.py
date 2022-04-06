import struct

filename = 'tile_placements_sorted.csv'
csv_indices = [0,1,2,3]

lines=[]
with open(filename) as file:
    lines = file.read().splitlines()

with open("data.bin", "wb") as f:
    for line in lines[1:]:
        data = line.split(',')

        f.write(int(data[csv_indices[0]]).to_bytes(8,byteorder='little'))
        f.write(int(data[csv_indices[1]]).to_bytes(2,byteorder='little'))
        f.write(int(data[csv_indices[2]]).to_bytes(2,byteorder='little'))
        f.write(int(data[csv_indices[3]]).to_bytes(2,byteorder='little'))
