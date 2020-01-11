using System;
using System.Collections.Generic;
using System.Text;

namespace Ancient_Wars_v1._0
{
    class Unit : Token
    {

        public Unit() : base()
        {
            mBaseSpeed = 3;
            mBaseHealth = 5;
            mBaseAttack = 2;
            SetToBaseValues();
        }

        public Unit(string nameArg, int speedArg, int healthArg, int attackArg, char iconArg): base(nameArg, iconArg)
        {
            mBaseSpeed = speedArg;
            mBaseHealth = healthArg;
            mBaseAttack = attackArg;            
            SetToBaseValues();
        }

        private void SetToBaseValues()
        {
            mMoveSpeedInt = mBaseSpeed;
            mCurrentAttackInt = mBaseAttack;
            mCurrentHealthInt = mBaseHealth;
        }

        public int TakeDamage(int damageTaken)
        {
            UnitHealth -= damageTaken;
            return UnitHealth;
        }

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

        public static Unit GetBasicUnit(int intArg)
        {
            switch (intArg)
            {
                case 0: return BASIC_MAGE;
                case 1: return BASIC_SOLDIER;
                case 2: return BASIC_RANGER;
                default: return BASIC_MAGE;
            }
        }

        private readonly static Unit BASIC_MAGE = new Unit("Mage", 4, 5, 2, 'M');
        private readonly static Unit BASIC_SOLDIER = new Unit("Soldier", 3, 4, 3, 'S');
        private readonly static Unit BASIC_RANGER = new Unit("Ranger", 2, 2, 4, 'R');


        private int mBaseHealth;
        private int mBaseSpeed;
        private int mBaseAttack;
        private int mMaxHealth;
        private int mMoveSpeedInt;

        public int MoveSpeed
        {
            get { return mMoveSpeedInt; }
            set { mMoveSpeedInt = value; }
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
