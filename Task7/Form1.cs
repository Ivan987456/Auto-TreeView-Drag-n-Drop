using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace Task7
{
    public partial class Form1 : Form
    {
        public List<Car> cars = new List<Car>();

        public Form1()
        {
            InitializeComponent();
            
            Initial();
            NodesAdd();
        }

        // первичное заполнение списка автомобиоей
        private void Initial()
        {
            cars.Add(new Car("Audi", "A6", 140, 1700, 200, 6, "Черный"));
            cars.Add(new Car("Audi", "Q7", 230, 2200, 210, 7, "Белый"));
            cars.Add(new Car("Audi", "R8", 510, 1200, 320, 4, "Зеленый"));
            cars.Add(new Car("Lada", "Kalina", 105, 1100, 170, 10, "красный"));
            cars.Add(new Car("Lada", "Priora", 115, 1300, 180, 9, "Серый"));
            cars.Add(new Car("Skoda", "Fabia", 90, 1050, 160, 11, "Коричневый"));
            cars.Add(new Car("Skoda", "Octavia", 210, 1500, 195, 7, "Серебристый"));
            cars.Add(new Car("Skoda", "Rapid", 190, 1400, 185, 9, "Желтый"));
        }

        // добавление нового автомобиля, данные заполняются из полей
        private void AddCar()
        {
            try
            {
                string tbName, tbModel, tbColor;
                int tbPower, tbWeight, tbSpeedMax, tbSpeed, select;
                select = treeView1.SelectedNode.Index;
                tbName = nameTextBox.Text;
                tbModel = modelTextBox.Text;
                tbColor = colorTextBox.Text;
                tbPower = Convert.ToInt32(powerTextBox.Text);
                tbWeight = Convert.ToInt32(weightTextBox.Text);
                tbSpeedMax = Convert.ToInt32(speedMaxTextBox.Text);
                tbSpeed = Convert.ToInt32(speedTextBox.Text);
                cars.Insert(select + 1, new Car(tbName, tbModel, tbPower, tbWeight, tbSpeedMax, tbSpeed, tbColor));
                treeView1.Nodes[0].Nodes[select].Nodes.Add(tbName);
            }
            catch
            {
                MessageBox.Show("Не верно заполены данные, заполните поля для добавления");
            }
        }

        // удаление
        private void Delete()
        {
            if (treeView1.Nodes[0].Nodes.Count > 0)
            {
                TreeNode nodeSelected = treeView1.SelectedNode;
                if (treeView1.SelectedNode.NextNode != null)
                    nodeSelected = treeView1.SelectedNode.NextNode;
                else if (treeView1.SelectedNode.PrevNode != null)
                    nodeSelected = treeView1.SelectedNode.PrevNode;
                treeView1.SelectedNode.Remove();
                treeView1.SelectedNode = nodeSelected;
            }
        }

        // заполнения дерева treeview1 элементами
        public void NodesAdd()
        {
            TreeNode automobil = new TreeNode("Автомобили");
            treeView1.Nodes.Add(automobil);
            automobil.Nodes.Add(cars[0].name);
            for (int i = 0; i < cars.Count - 1; i++)
            {
                if (cars[i].name != cars[i + 1].name)
                    automobil.Nodes.Add(cars[i + 1].name);
            }

            for (int i = 0; i < automobil.Nodes.Count; i++)
            {
                for (int j = 0; j < cars.Count; j++)
                    if ((string)automobil.Nodes[i].Text == (string)cars[j].name)
                        automobil.Nodes[i].Nodes.Add(cars[j].model);
            }
        }

        // выбор элемента в древе
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string model;
            if (treeView1.Nodes[0].Nodes.Count > 0)
            {
                TreeNode nodeSelected = treeView1.SelectedNode;
                if (nodeSelected.Parent != null)
                {
                    model = nodeSelected.Text.Split('-')[0];
                    foreach (Car car in cars)
                    {
                        if (car.model.ToString().Equals(model))
                        {
                            nameTextBox.Text = car.name;
                            modelTextBox.Text = car.model;
                            powerTextBox.Text = Convert.ToString(car.power);
                            weightTextBox.Text = Convert.ToString(car.weight);
                            speedMaxTextBox.Text = Convert.ToString(car.speedMax);
                            speedTextBox.Text = Convert.ToString(car.speed);
                            colorTextBox.Text = car.color;
                        }
                    }
                }
            }
        }

        // вызов метода добавление автомобиля
        private void button1_Click(object sender, EventArgs e)
        {
            AddCar();
        }

        private void panel3_DragDrop(object sender, DragEventArgs e)
        {
            Delete();
        }

        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            treeView1.DoDragDrop(treeView1.SelectedNode, DragDropEffects.Move);
        }

        // отпускание мыши над корзиной при перетаскивании
        private void panel3_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        // сериализация
        private void button2_Click(object sender, EventArgs e)
        {
            var xmlFormatter = new XmlSerializer(typeof(List<Car>));

            using (var file = new FileStream("Car.xml", FileMode.OpenOrCreate))
            {
                xmlFormatter.Serialize(file, cars);
            }
        }

        // десериализация
        private void button3_Click(object sender, EventArgs e)
        {
            var xmlFormatter = new XmlSerializer(typeof(List<Car>));

            using (var file = new FileStream("Car.xml", FileMode.OpenOrCreate))
            {
                var newCar = xmlFormatter.Deserialize(file) as List<Car>;

                cars = newCar;
            }
            treeView1.Nodes.Clear();
            NodesAdd();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string search = textBox1.Text;
            foreach (Car car in cars)
            {
                if (search == car.model)
                {
                    nameTextBox.Text = car.name;
                    modelTextBox.Text = car.model;
                    powerTextBox.Text = Convert.ToString(car.power);
                    weightTextBox.Text = Convert.ToString(car.weight);
                    speedMaxTextBox.Text = Convert.ToString(car.speedMax);
                    speedTextBox.Text = Convert.ToString(car.speed);
                    colorTextBox.Text = car.color;
                }
            }
        }
    }
}
