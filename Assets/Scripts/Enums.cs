using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Enums
{
    public enum Layers
    {
        EnemyBullets = 9,
        PlayerBullets = 12
    }

    public enum ControllerType
    {
        MouseKeyboard,
        XBoxController
    }

    public enum FloatingTextType
    {
        Text,
        Damage,
        Heal
    }

    public enum EnemyMessagesRarity
    {
        Common = 12,
        LessCommon = 9,
        SemiRare = 6,
        Rare = 3,
        VeryRare = 1
    }

    public class Items
    {
        public enum Type
        {
            Money,
            Health
        }

        public enum CollectType
        {
            Instant,
            Collect
        }

        public enum Rarity
        {
            Common = 12,
            LessCommon = 9,
            SemiRare = 6,
            Rare = 3,
            VeryRare = 1
        }
    }

    public class Damage
    {
        public enum Type
        {
            Normal,
            Explosion
        }
    }

    public class Angles
    {
        public enum X
        {
            Right,
            Left
        }

        public enum Y
        {
            Top,
            Bottom
        }
    }
}
