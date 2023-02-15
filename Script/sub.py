# python3.6

import json
import random

from paho.mqtt import client as mqtt_client
from mysql_insert import data_append
import pymysql

conn = pymysql.connect(host='127.0.0.1', port=3306,
                       user='root', password='Db@2013.', db='inventory')

broker = 'localhost'
port = 1883
topic = "Inventory"
# generate client ID with pub prefix randomly
client_id = f'python-mqtt-{random.randint(0, 100)}'


def connect_mqtt() -> mqtt_client:
    def on_connect(client, userdata, flags, rc):
        if rc == 0:
            print("Connected to MQTT Broker!")
        else:
            print("Failed to connect, return code %d\n", rc)

    client = mqtt_client.Client(client_id)
    client.on_connect = on_connect
    client.connect(broker, port)
    return client


def subscribe(client: mqtt_client):
    def on_message(client, userdata, msg):
        # print(f"Received `{msg.payload.decode()}` from `{msg.topic}` topic")
        # bytes
        # print(type(msg.payload))

        # loads ...
        payload = json.loads(msg.payload)
        # print(type(payload))

        print(payload["CPU"])
        print(payload["RAM"])
        print(payload["DISK"])
        print(payload["VideoController"])
        print(payload["NetworkAdapter"])
        print(payload["SoundCard"])
        print(payload["Monitor"])

        print(payload["SN"])
        print(payload["Brand"])
        print(payload["Model"])
        print(payload["OS"])
        print(payload["HostName"])
        print(payload["IPAddr"])
        print(payload["LastCheckin"])

        CPU = (payload["CPU"])
        RAM = (payload["RAM"])
        DISK = (payload["DISK"])
        VideoController = (payload["VideoController"])
        NetworkAdapter = (payload["NetworkAdapter"])
        SoundCard = (payload["SoundCard"])
        Monitor = (payload["Monitor"])

        SN = (payload["SN"])
        Brand = (payload["Brand"])
        Model = (payload["Model"])
        OS = (payload["OS"])
        HostName = (payload["HostName"])
        IPAddr = (payload["IPAddr"])
        
        LastCheckin = (payload["LastCheckin"])

        values = (CPU, RAM, DISK, VideoController,NetworkAdapter,SoundCard,Monitor,SN,Brand,Model,OS,HostName,IPAddr,LastCheckin)
        data_append(conn=conn,values=values)

    client.subscribe(topic)
    client.on_message = on_message


def run():
    client = connect_mqtt()
    subscribe(client)
    client.loop_forever()


if __name__ == '__main__':
    run()
