import time
import serial

serial = serial.Serial('/dev/ttyS0', 9600, timeout=1)
serial.reset_input_buffer()
serial.flush()

def print_gps_data():
    print(line.rstrip())

while True:
    try:
        line = serial.readline().decode('utf-8')

        while len(line) > 0:
            print_gps_data()
            line = serial.readline().decode('utf-8')

    except UnicodeDecodeError:
        line = serial.readline().decode('utf-8')

    time.sleep(1)