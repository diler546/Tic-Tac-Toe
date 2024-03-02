using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    public partial class Form1 : Form
    {
        static bool zeroMove = false;
        static bool computerMove = true;
        static string winner;
        static string computerSymbol;
        static string PlayerSymbol;
        public Form1()
        {
            InitializeComponent();            
        }

        public void CharacterSelection()
        {
            label2.Visible= false;
            label3.Visible= false;
            label4.Visible= true;
            button12.Visible= false;
            button13.Visible= false;
            button14.Visible= true;
            button15.Visible= true;
        }

        public void ButtonX(object sender, EventArgs e)
        {
            computerSymbol = "O";
            PlayerSymbol = "X";
            SwitchingToGameMode();
        }

        public void ButtonO(object sender, EventArgs e)
        {
            computerSymbol = "X";
            PlayerSymbol = "O";
            SwitchingToGameMode();
        }

        public void DefinitionOfTheFirstInUse()
        {
            int choice = new Random().Next(0, 2);
            if (choice == 0)
            {
                label1.Text = "Ход: O";
                zeroMove = true;
            }
            else
            {
                label1.Text = "Ход: X";
                zeroMove = false;
            }
        }

        public bool CheckWin(out string winner)
        {
            winner = "";
            // Проверка строк
            if (button1.Text == button2.Text && button2.Text == button3.Text && !string.IsNullOrEmpty(button1.Text))
            {
                winner = button1.Text;
                return true;
            }
            if (button4.Text == button5.Text && button5.Text == button6.Text && !string.IsNullOrEmpty(button4.Text))
            {
                winner = button4.Text;
                return true;
            }
            if (button7.Text == button8.Text && button8.Text == button9.Text && !string.IsNullOrEmpty(button7.Text))
            {
                winner = button7.Text;
                return true;
            }
            // Проверка столбцов
            if (button1.Text == button4.Text && button4.Text == button7.Text && !string.IsNullOrEmpty(button1.Text))
            {
                winner = button1.Text;
                return true;
            }
            if (button2.Text == button5.Text && button5.Text == button8.Text && !string.IsNullOrEmpty(button2.Text))
            {
                winner = button2.Text;
                return true;
            }
            if (button3.Text == button6.Text && button6.Text == button9.Text && !string.IsNullOrEmpty(button3.Text))
            {
                winner = button3.Text;
                return true;
            }
            // Проверка диагоналей
            if ((button1.Text == button5.Text && button5.Text == button9.Text && !string.IsNullOrEmpty(button1.Text)) ||
               (button3.Text == button5.Text && button5.Text == button7.Text && !string.IsNullOrEmpty(button3.Text)))
            {
                winner = button5.Text;
                return true;
            }

            return false;
        }

        public void Restart(object sender, EventArgs e)
        {
            for (int i = 1; i < 10; i++)
            {
                string buttonName = "button" + i;
                Button button = this.Controls.Find(buttonName, true).FirstOrDefault() as Button;
                button.Text ="";
                button.Enabled = true;
            }
            DefinitionOfTheFirstInUse();
            if (computerMove)
            {
                label1.Text = "Вы: "+PlayerSymbol;
                ComputerMove();
            }
        }

        public bool Draw()
        {
            for (int i = 1; i < 10; i++)
            {
                string buttonName = "button" + i;
                Button button = this.Controls.Find(buttonName, true).FirstOrDefault() as Button;
                if(button.Text == "")
                {
                    return false;
                }
            }
            return true;
        }
        public void IdleMode()
        {
            for (int i = 1; i < 10; i++)
            {
                string buttonName = "button" + i;
                Button button = this.Controls.Find(buttonName, true).FirstOrDefault() as Button;
                button.Enabled = false;
            }
        }
        public void ComputerMove()
        {
            while(true)
            {
                if (zeroMove)
                {
                    return;
                }
                if (CheckWin(out winner))
                {
                    return;
                }
                else if (Draw())
                {
                    return;
                }
                int choice = new Random().Next(1, 10);
                for (int i = 1; i < 10; i++)
                {
                    string buttonName = "button" + i;
                    Button button = this.Controls.Find(buttonName, true).FirstOrDefault() as Button;
                    if(choice == i && button.Text=="")
                    {
                        button.Enabled = false;
                        button.Text = computerSymbol;
                        return;
                    }
                }
            }
        }

        public void ButtonClickReturnMenu(object sender, EventArgs e)
        {
            for (int i = 1; i < 12; i++)
            {
                string buttonName = "button" + i;
                Button button = this.Controls.Find(buttonName, true).FirstOrDefault() as Button;
                button.Visible = false;
            }
            label1.Visible = false;

            button12.Visible = true;
            button13.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
        }

        public void SwitchingToGameMode()
        {
            object sender=null;
            EventArgs e=null;           
            button12.Visible = false;
            button13.Visible = false;
            button14.Visible = false;
            button15.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;

            for (int i = 1; i < 12; i++)
            {
                string buttonName = "button" + i;
                Button button = this.Controls.Find(buttonName, true).FirstOrDefault() as Button;
                button.Visible = true;
            }
            label1.Visible = true;
            Restart(sender,e);
        }
        public void ButtonClickСomputerGameMode(object sender, EventArgs e)
        {
            computerMove = true;
            CharacterSelection();    
        }
        public void ButtonClickTwoPlayerMode(object sender, EventArgs e)
        {
            computerMove = false;
            SwitchingToGameMode();
        }

        public void ButtonClick(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            

            if (computerMove)
            {
                clickedButton.Text = PlayerSymbol;
                clickedButton.Enabled = false;
                zeroMove = false;
                ComputerMove();

            }
            if (!(computerMove))
            {
                if (zeroMove)
                {
                    clickedButton.Text = "O";
                    zeroMove = false;
                    label1.Text = "Ход: X";
                    clickedButton.Enabled = false;
                }
                else
                {
                    clickedButton.Text = "X";
                    zeroMove = true;
                    label1.Text = "Ход: O";
                    clickedButton.Enabled = false;
                }

            }
            if (CheckWin(out winner))
            {
                MessageBox.Show($"Выйграли {winner}", "Победитель");
                IdleMode();

            }
            else if (Draw())
            {
                MessageBox.Show("Вы привели игру к нечьей", "Ничья");
            }
        }
    }
}
