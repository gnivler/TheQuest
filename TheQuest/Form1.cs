using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TheQuest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Game game;
        private Random random = new Random();

        private void Form1_Load(object sender, EventArgs e)
        {
            game = new Game(new Rectangle(78, 57, 420, 155));
            game.NewLevel(random);
            UpdateCharacters();
        }

        public void UpdateCharacters()
        {
            avatar.Location = game.PlayerLocation;
            playerHitPoints.Text = game.PlayerHitPoints.ToString();
            bool showBat = false;
            bool showGhost = false;
            bool showGhoul = false;
            int enemiesShown = 0;

            // update enemy visibility and location
            foreach (Enemy enemy in game.Enemies)
            {
                if (enemy is Bat)
                {
                    bat.Location = enemy.Location;
                    batHitPoints.Text = enemy.HitPoints.ToString();
                    if (enemy.HitPoints > 0)
                    {
                        showBat = true;
                        bat.Visible = true;
                        enemiesShown++;
                    }
                }
                if (enemy is Ghost)
                {
                    ghost.Location = enemy.Location;
                    ghostHitPoints.Text = enemy.HitPoints.ToString();
                    if (enemy.HitPoints > 0)
                    {
                        showGhost = true;
                        ghost.Visible = true;
                        enemiesShown++;
                    }
                }
                if (enemy is Ghoul)
                {
                    ghoul.Location = enemy.Location;
                    ghoulHitPoints.Text = enemy.HitPoints.ToString();
                    if (enemy.HitPoints > 0)
                    {
                        showGhoul = true;
                        ghoul.Visible = true;
                        enemiesShown++;
                    }
                }
            }

            if (!showBat)
            {
                bat.Visible = false;
                batLabel.Visible = false;
                batHitPoints.Text = "";
            }
            if (!showGhost)
            {
                ghost.Visible = false;
                ghostLabel.Visible = false;
                ghostHitPoints.Text = "";
            }
            if (!showGhoul)
            {
                ghoul.Visible = false;
                ghoulLabel.Visible = false;
                ghoulHitPoints.Text = "";
            }

            // update visibility for items on the floor
            sword.Visible = false;
            bow.Visible = false;
            mace.Visible = false;
            redPotion.Visible = false;
            bluePotion.Visible = false;
            Control weaponControl = null;
            switch (game.WeaponInRoom.Name)
            {
                case "Sword":
                    weaponControl = sword; break;
                case "Bow":
                    weaponControl = bow; break;
                case "Mace":
                    weaponControl = mace; break;
                case "Blue Potion":
                    weaponControl = bluePotion; break;
                case "Red Potion":
                    weaponControl = redPotion; break;
            }
            weaponControl.Visible = true;

            // update inventory icon visibility
            if (game.CheckPlayerInventory("Sword"))
            {
                inventorySword.Visible = true;
            }
            if (game.CheckPlayerInventory("Bow"))
            {
                inventoryBow.Visible = true;
            }
            if (game.CheckPlayerInventory("Mace"))
            {
                inventoryMace.Visible = true;
            }
            if (game.CheckPlayerInventory("Blue Potion"))
            {
                inventoryBluePotion.Visible = true;
            }
            if (game.CheckPlayerInventory("Red Potion"))
            {
                inventoryRedPotion.Visible = true;
            }

            weaponControl.Location = game.WeaponInRoom.Location;
            if (game.WeaponInRoom.PickedUp)
            {
                weaponControl.Visible = false;
            }
            else
            {
                weaponControl.Visible = true;
            }

            if (game.PlayerHitPoints <= 0)
            {
                MessageBox.Show("You died");
                Application.Exit();
            }

            if (enemiesShown < 1)
            {
                MessageBox.Show("You have defeated the enemies on this level");
                game.NewLevel(random);
                UpdateCharacters();
            }
        }

        private void moveUp_Click(object sender, EventArgs e)
        {
            game.Move(Direction.Up, random);
            UpdateCharacters();
        }

        private void moveRight_Click(object sender, EventArgs e)
        {
            game.Move(Direction.Right, random);
            UpdateCharacters();
        }

        private void moveDown_Click(object sender, EventArgs e)
        {
            game.Move(Direction.Down, random);
            UpdateCharacters();
        }

        private void moveLeft_Click(object sender, EventArgs e)
        {
            game.Move(Direction.Left, random);
            UpdateCharacters();
        }

        private void attackUp_Click(object sender, EventArgs e)
        {
            game.Attack(Direction.Up, random);
            UpdateCharacters();
        }

        private void attackLeft_Click(object sender, EventArgs e)
        {
            game.Attack(Direction.Left, random);
            UpdateCharacters();
        }

        private void attackRight_Click(object sender, EventArgs e)
        {
            game.Attack(Direction.Right, random);
            UpdateCharacters();
        }

        private void attackDown_Click(object sender, EventArgs e)
        {
            game.Attack(Direction.Down, random);
            UpdateCharacters();
        }

        private void inventorySword_Click(object sender, EventArgs e)
        {
            game.Equip("Sword");
            inventorySword.BorderStyle = BorderStyle.FixedSingle;
        }

        private void inventoryBow_Click(object sender, EventArgs e)
        {

        }

        private void inventoryMace_Click(object sender, EventArgs e)
        {

        }

        private void inventoryBluePotion_Click(object sender, EventArgs e)
        {

        }

        private void inventoryRedPotion_Click(object sender, EventArgs e)
        {

        }
    }
}
