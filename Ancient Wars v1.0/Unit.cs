using System;
using System.Collections.Generic;
using System.Text;

namespace Ancient_Wars_v1._0
{
    //Any cretaure is a unit; They have health, move, and attack values
    class Unit : Token
    {
        
        //Instantiate a basic unit
        public Unit() : base()
        {
            mBaseSpeed = 3;
            mBaseHealth = 5;
            mBaseAttack = 2;
            SetToBaseValues();
        }

        //Instantiate a unit specifying all values
        public Unit(string nameArg, char teamCharArg, int speedArg, int healthArg, int attackArg, int rangeArg, char iconArg): base(nameArg, iconArg, teamCharArg)
        {
            mBaseSpeed = speedArg;
            mBaseHealth = healthArg;
            mBaseAttack = attackArg;
            mBaseRange = rangeArg;
            SetToBaseValues();
            mAliveBool = true;
        }

        //Sets all current values to the instantiated values
        private void SetToBaseValues()
        {
            mMoveSpeedInt = mBaseSpeed;
            mCurrentAttackInt = mBaseAttack;
            mCurrentHealthInt = mBaseHealth;
        }

        //This method returns a very basic version of a unit
        public static Unit GetBasicUnit(int intArg)
        {
            Unit lBaseUnit= All_BASIC_UNITS[intArg];
            Unit lUnit = new Unit(lBaseUnit.Name, lBaseUnit.TokenID.IDChar, lBaseUnit.MoveSpeed, lBaseUnit.UnitHealth, lBaseUnit.Attack, lBaseUnit.AttackRange, lBaseUnit.Icon);
            return lUnit;
        }

        //Defines the basic units
        private readonly static Unit BASIC_MAGE = new Unit("Mage", 'N',4, 5, 2, 3, 'M');
        private readonly static Unit BASIC_SOLDIER = new Unit("Soldier", 'A',3, 4, 3, 1, 'S');
        private readonly static Unit BASIC_RANGER = new Unit("Ranger", 'P', 2, 2, 4, 4,'R');

        public readonly static Unit[] All_BASIC_UNITS = 
        {
            BASIC_MAGE,
            BASIC_SOLDIER,
            BASIC_RANGER
        };

        

        //Override ActionList from token

        
        //Decrease this unit's health by a given integer
        public int TakeDamage(int damageTaken)
        {
            UnitHealth -= damageTaken;
            return UnitHealth;
        }

        //Launch an attack on another unit
        public bool AttackUnit(Unit unitArg)
        {
            int remainingHealth = unitArg.TakeDamage(this.Attack);
            if (remainingHealth <= 0)
            {
                unitArg.Alive = false;
                return false;
            }
            return true;
        }

        public string GetUnitInfo()
        {
            StringBuilder infoBuilder = new StringBuilder();
            infoBuilder.Append(this.Name + " (" + this.Icon + ") ID:" + this.TokenID + "\r\n");
            infoBuilder.Append("[Current]/[Max]/[Base]" + "\r\n");
            infoBuilder.Append("Move Speed:" + this.MoveSpeed + "/" + "[N/A]" + "/" + this.mBaseSpeed + "\r\n");
            infoBuilder.Append("Health:" + this.UnitHealth + "/" + this.mMaxHealth + "/" + this.mBaseHealth + "\r\n");
            infoBuilder.Append("Attack:" + this.Attack + "/" + "[N/A]" + "/" + this.mBaseAttack + "\r\n");

            return infoBuilder.ToString();
        }

        /// <summary>
        /// ==============
        /// Properties
        /// ==============
        /// </summary>
        private int mBaseHealth;
        private int mBaseSpeed;
        private int mBaseAttack;
        private int mBaseRange;
        private int mMaxHealth;
        private int mMoveSpeedInt;

        public int MoveSpeed
        {
            get { return mMoveSpeedInt; }
            private set { mMoveSpeedInt = value; }
        }

        private int mCurrentHealthInt;

        public int UnitHealth
        {
            get { return mCurrentHealthInt; }
            private set { mCurrentHealthInt = value; }
        }

        private int mCurrentAttackInt;

        public int Attack
        {
            get { return mCurrentAttackInt; }
            set { mCurrentAttackInt = value; }
        }

        private int mCurrentAttackRange;

        public int AttackRange
        {
            get { return mCurrentAttackRange; }
            set { mCurrentAttackRange = value; }
        }


        private bool mAliveBool;

        public bool Alive
        {
            get { return mAliveBool; }
            private set { mAliveBool = value; }
        }
    }
}
