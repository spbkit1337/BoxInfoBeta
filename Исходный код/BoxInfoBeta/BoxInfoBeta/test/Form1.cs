using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



//мои юзынги :)

//Самая главная библеотека блогадаря которой возможно получить данные о температуре(и не только) процессора , видеокарты , материнки и т.д
using OpenHardwareMonitor.Hardware; 
using OpenHardwareMonitor.Collections;




namespace BoxInfoBeta
{
    public partial class Form1 : Form
    {
        //Важное уточнение сначало я создал это приложение с названием test и потом решил поменять на BoxInfoBeta . Я поменял везде плюс надо было в файле program.cs добавить вот такую строку:  using BoxInfoBeta;

        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 1000; //я создал таймер чтобы каждую 1 сек обновлялся label с температурой
            timer1.Start();

           
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //здесь я присвоил пустые значения для того чтобы при загрузке формы не было надписей label1 и 4 для красоты :)
            label1.Text = "";
            label4.Text = "";
        }



        //тут создаю переменные с плавающей запятой (float) для процессора и видюхи

        // CPU Temperature
        static float cpuTemp;

        // GPU Temperature
        static float gpuTemp;



        //Таймер
        private void timer1_Tick(object sender, EventArgs e)
        {

            //определяем новый объект компьютера, предоставляемый библиотекой DLL Open Hardware Monitor
            Computer myComputer = new Computer();


            //Эти параметры указывают библиотеке Open Hardware Monitor возвращать данные для любых процессоров или видеокарт, подключенных в данный момент к системе
            myComputer.GPUEnabled = true; 

            myComputer.CPUEnabled = true;


            //устанавливаем соединение между  кодом и файлом DLL
            myComputer.Open();




            //GPU видеокарта (с проверкой больше и меньше чтобы менять цвет)
            foreach (var hardwareItem in myComputer.Hardware)
            {
                if (hardwareItem.HardwareType == HardwareType.GpuAti || hardwareItem.HardwareType == HardwareType.GpuNvidia)
                {
                    hardwareItem.Update();
                    foreach (var sensor in hardwareItem.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature)
                        {
                            //присвавываем  переменной число с датчика
                            gpuTemp = sensor.Value.GetValueOrDefault();

                            //выводим в label
                            //тут условие если температура меньше 50 то надпись будет зеленой
                            if (gpuTemp < 50)
                            {
                                label1.ForeColor = Color.Green;
                                label1.Text = "" + gpuTemp.ToString() + " °C";
                            }
                            else if (gpuTemp > 50) //если температура больше 50 то надпись будет красной
                            {
                                label1.ForeColor = Color.Red;
                                label1.Text = "" + gpuTemp.ToString() + " °C";
                            }

                        }
                    }
                }
            }


            //CPU процессор (с проверкой больше и меньше чтобы менять цвет)
            foreach (var hardware in myComputer.Hardware)
            {
                if (hardware.HardwareType == HardwareType.CPU)
                {
                    hardware.Update();
                    foreach (var sensor in hardware.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature)
                        {
                            //присвавываем  переменной число с датчика
                            cpuTemp = sensor.Value.GetValueOrDefault();

                            //выводим в label
                            //тут условие если температура меньше 50 то надпись будет зеленой
                            if (cpuTemp < 50)
                            {
                                label4.ForeColor = Color.Green;
                                label4.Text = "" + cpuTemp.ToString() + " °C";
                            }
                            else if (cpuTemp > 50) //если температура больше 50 то надпись будет красной
                            {
                                label4.ForeColor = Color.Red;
                                label4.Text = "" + cpuTemp.ToString() + " °C";
                            }

                        }
                    }
                }
            }


            //Вывод температуры видеокарты с переменной
            ////GPU
            //foreach (var hardwareItem in myComputer.Hardware)
            //{
            //    if (hardwareItem.HardwareType == HardwareType.GpuAti || hardwareItem.HardwareType == HardwareType.GpuNvidia)
            //    {
            //        hardwareItem.Update();
            //        foreach (var sensor in hardwareItem.Sensors)
            //        {
            //            if (sensor.SensorType == SensorType.Temperature)
            //            {
            //                //присвавываем  переменной число с датчика
            //                gpuTemp = sensor.Value.GetValueOrDefault();

            //                //выводим в label
            //                label1.Text = "" + gpuTemp.ToString() + " °C";

            //            }
            //        }
            //    }
            //}



            //Вывод температуры процессора с переменной
            ////CPU
            //foreach (var hardware in myComputer.Hardware)
            //{
            //    if (hardware.HardwareType == HardwareType.CPU)
            //    {
            //        hardware.Update();
            //        foreach (var sensor in hardware.Sensors)
            //        {
            //            if (sensor.SensorType == SensorType.Temperature)
            //            {
            //                //присвавываем  переменной число с датчика
            //                cpuTemp = sensor.Value.GetValueOrDefault();

            //                //выводим в label
            //                label4.Text = "" + cpuTemp.ToString() + " °C";

            //            }
            //        }
            //    }
            //}



            //вывод температура видеокарты без переменных
            ////GPU
            //foreach (var hardwareItem in myComputer.Hardware)
            //{

            //    if (hardwareItem.HardwareType == HardwareType.GpuAti || hardwareItem.HardwareType == HardwareType.GpuNvidia)
            //    {
            //        foreach (var sensor in hardwareItem.Sensors)
            //        {
            //            if (sensor.SensorType == SensorType.Temperature)
            //            {

            //                label1.Text = " " + sensor.Value.GetValueOrDefault() + " °C";
            //            }
            //        }
            //    }
            //}



        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/spbkit1337");
        }
    }


    
}
