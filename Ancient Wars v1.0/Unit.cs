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
        public Unit(string nameArg, int speedArg, int healthArg, int attackArg, char iconArg): base(nameArg, iconArg)
        {
            mBaseSpeed = speedArg;
            mBaseHealth = healthArg;
            mBaseAttack = attackArg;            
            SetToBaseValues();
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
            Unit lUnit = All_BASIC_UNITS[intArg];
            return lUnit;
        }

        //Defines the basic units
        private readonly static Unit BASIC_MAGE = new Unit("Mage", 4, 5, 2, 'M');
        private readonly static Unit BASIC_SOLDIER = new Unit("Soldier", 3, 4, 3, 'S');
        private readonly static Unit BASIC_RANGER = new Unit("Ranger", 2, 2, 4, 'R');

        public readonly static Unit[] All_BASIC_UNITS = 
        {
            BASIC_MAGE,
            BASIC_SOLDIER,
            BASIC_RANGER
        };

        

        //Override ActionList from token

        public override string[] ActionList()
        {
            string[] lList =
            {
                "0 - List Unit Data; N/I",
                "1 - Show Possible Unit Moves; N/I",
                "2 - Show Possible Unit Targets; N/I",
                "3 - Move Unit; N/I",
                "4 - Attack Another Unit; N/I"
            };
            return lList;
        }



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

        /// <summary>
        /// ==============
        /// Properties
        /// ==============
        /// </summary>
        private int mBaseHealth;
        private int mBaseSpeed;
        private int mBaseAttack;
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

        private bool mAliveBool;

        public bool Alive
        {
            get { return mAliveBool; }
            private set { mAliveBool = value; }
        }
    }
}
