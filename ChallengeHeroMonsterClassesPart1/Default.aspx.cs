using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChallengeHeroMonsterClassesPart1
{

    public partial class Default : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Character hero = new Character();

            hero.Name = "Maximus";
            hero.Health = 100;
            hero.DamageMaximum = 40;
            hero.AttackBounus = true;


            Character monster = new Character();

            monster.Name = "Cyclops";
            monster.Health = 100;
            monster.DamageMaximum = 45;
            monster.AttackBounus = false;

            Dice dice = new Dice();

            // This is the bonus round
            if (hero.AttackBounus)
                monster.Defend(hero.Attack(dice));
            if (monster.AttackBounus)
                hero.Defend(monster.Attack(dice));

            while (monster.Health > 0 && hero.Health > 0)
            {
                hero.Defend(monster.Attack(dice));
                monster.Defend(hero.Attack(dice));
                printResult(hero);
                printResult(monster);
            }

            displayResult(hero, monster);



        }

        private void displayResult( Character opponent1, Character opponent2)
        {
            if (opponent1.Health <= 0 && opponent2.Health <= 0)
                resultLabel.Text += string.Format("Both {0} and {1} died.", opponent1.Name, opponent2.Name);
            else if (opponent1.Health <= 0)
                resultLabel.Text += string.Format("{0} defeats {1}!!", opponent2.Name, opponent1.Name);
            else
                resultLabel.Text += string.Format("{0} defeats {1}!!", opponent1.Name, opponent2.Name);
        }

        private void printResult(Character character)
        {
            resultLabel.Text += string.Format("Name: {0} - Health: {1} - DamageMaximum: {2} - AttackBonus: {3}<br/>", 
                character.Name, character.Health, character.DamageMaximum, character.AttackBounus);
        }
    }

    class Character
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int DamageMaximum { get; set; }
        public bool AttackBounus { get; set; }

        
        public int Attack(Dice dice)
        {
            dice.Sides = this.DamageMaximum;
            return dice.Roll();
        }

        public void Defend(int attackDamage)
        {
            this.Health -= attackDamage;
        }
    }


    class Dice
    {
        public int Sides { get; set; }

        Random random = new Random();

        public int Roll()
        {
            return random.Next(this.Sides);
        }
    }
}