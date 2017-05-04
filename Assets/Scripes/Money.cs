using UnityEngine;
using System.Collections;
public class Money
{   
    public int goldCoin=0;
    public int sliverCoin=0;
    public int copperCoin=0;
    public enum Coin{ 金币,银币,铜币 }
    public enum Food { 馒头,稀粥,玉米}
    public enum Prop { 防御,破防,偷窃,无敌}
    public int  GetIntegral()
    {
        return goldCoin*3+ sliverCoin*2+ copperCoin;
    }
    public bool SetCoin(Coin coin,int num) {
        switch (coin)
        {
           
            case Coin.金币:
                if (goldCoin + num >= 0)
                {
                    goldCoin += num;
                    return true;
                }
                break;
            case Coin.银币:
                if (sliverCoin + num >= 0)
                {
                    sliverCoin += num;
                    return true;
                }
                break;
            case Coin.铜币:
                if (copperCoin + num >= 0)
                {
                    copperCoin += num;
                    return true;
                }
                break;

        }
        return false;
        
    }
    public static int FoodMoney(Money.Food food)
    {
        switch(food)
        {
            case Food.馒头:
                return -1;
            case Food.稀粥:
                return -2;
            case Food.玉米:
                return -3;
            default:
                return 0;
        }
    }
    public static int FoodAdd(Money.Food food)
    {
        switch (food)//10,17,25
        {
            case Food.馒头:
                return 20;
            case Food.稀粥:
                return 50;
            case Food.玉米:
                return 80;
            default:
                return 0;
        }
    }
    public static int PropMoney(Money.Prop prop)
    {
        switch (prop)//10,17,25
        {
            case Prop.防御:
                return -1;
            case Prop.破防:
                return -1;
            case Prop.偷窃:
                return -5;
            case Prop.无敌:
                return -10;
            default:
                return 0;
        }
    }
}
