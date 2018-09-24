﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp_Meteo
{
    class Program
    {
        static void Main(string[] args)
        {
            WeatherEngine engine = new WeatherEngine();

            engine.WeatherChange += Engine_WeatherChange;
            engine.GetWeather(100);
           
            Console.ReadLine();
        }

        private static void Engine_WeatherChange(object sender, WeatherEngine.MeteoEventArgs meteo)
        {
            Console.WriteLine(meteo.Meteo);
        }
    }
    public enum Meteo {soleil, nuageux, pluvieux, orageux };

    

    public class WeatherEngine
    {
        private Random random = new Random();
        private Meteo _meteoActuelle;

        //public int[] GetWeather(int nb)
        public void GetWeather(int nb)
        {
            //int[] weatherStats = new int[nb];
            for (int i = 0; i <= nb; i++)
            {
                //weatherStats[i] = random.Next(0, 101);
                //weatherStats[i].SayWeather();
                int n = random.Next(0, 101);
                Meteo meteo = n.SayWeather();
                this.MeteoActuelle = meteo;
            }
            //return weatherStats; 
        }

        public class MeteoEventArgs : EventArgs
        {
            public MeteoEventArgs(Meteo meteo)
            {
                this.Meteo = meteo;
            }

            public Meteo Meteo
            {
                get; private set;
            }
        }

        //public delegate void WeatherDelegate(object sender, MeteoEventArgs meteo);
        //public event WeatherDelegate WeatherChange;
        public event EventHandler<MeteoEventArgs> WeatherChange;

        public Meteo MeteoActuelle
        {
            get { return _meteoActuelle; }
            set
            {
                if (_meteoActuelle != value)
                {
                    _meteoActuelle = value;
                    RaiseWeatherChange(value);
                }
            }
                
        }

        private void RaiseWeatherChange(Meteo value)
        {
            var handler = WeatherChange;
            if (handler != null)
            {
                MeteoEventArgs args = new MeteoEventArgs(value);
                handler(this, args);
            }
        }


    }

    public static class IntegerExtension
    {
        public static Meteo SayWeather(this int rand)
        {
            switch (rand)
            {
                case var sun when sun < 5:
                    return Meteo.soleil;
                case var cloud when cloud < 50:
                    return Meteo.nuageux;
                case var rain when rain < 90:
                    return Meteo.pluvieux;
                default:
                    return Meteo.orageux;

            }
        }
    }

    

}
