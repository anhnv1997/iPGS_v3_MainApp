using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParking.Object
{
    public class MapUpdateInfor
    {
        private int posX, posY, width, height, zoneLocation;
        private string mapID, zoneID;
        public MapUpdateInfor(string mapID, string zoneID)
        {
            this.mapID = mapID;
            this.zoneID = zoneID;
        }
        public MapUpdateInfor(string mapID, string zoneID, int posX, int posY, int width, int height, int mapHeight, int mapWidth)
        {
            this.mapID = mapID;
            this.zoneID = zoneID;
            this.posX = posX;
            this.posY = posY;
            this.width = width;
            this.height = height;
            this.mapWidth = mapWidth;
            this.MapHeight = mapHeight;
        }

        public string MapID { get => mapID; set => mapID = value; }
        public string ZoneID { get => zoneID; set => zoneID = value; }
        public int PosX { get => posX; set => posX = value; }
        public int PosY { get => posY; set => posY = value; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public int MapHeight { get; set; }
        public int mapWidth { get; set; }
    }
}

