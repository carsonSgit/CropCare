import time
import serial
import pynmea2

serial = serial.Serial('/dev/ttyS0', 9600, timeout=1)
serial.reset_input_buffer()
serial.flush()

def parse_gps_data(line):
    if line.find('GGA') > 0:
        try:
            msg = pynmea2.parse(line)
            print(f"Timestamp: {msg.timestamp} -- Lat: {msg.latitude} {msg.lat_dir} -- Lon: {msg.longitude} {msg.lon_dir} -- Altitude: {msg.altitude} {msg.altitude_units} -- Satellites: {msg.num_sats}")
        except pynmea2.ParseError as e:
            print(f"Parse error: {e}")

while True:
    try:
        line = serial.readline().decode('utf-8')
        parse_gps_data(line)
    except UnicodeDecodeError:
        print("UnicodeDecodeError encountered")
    time.sleep(1)
