public static class SkillData
{
    //스킬이름은 항상 Pefeb이름과 같아야 한다. 그래야 Resources 폴더에서 가져옴
    public enum Skills
    {
        /**********1000**********/
        FireBall_Store = 1000,  SawBlade_Store, WaveEnergy_Store,   Quickness_Store, Redraw_Store,
        /**********900***********/

        /**********800***********/
        Tornado_Store = 800,    Spark_Store,    Clairvoyant_Store,  Trident_Store, Redraw2_Store, Redraw3_Store,
        /**********700***********/

        /**********600***********/

        /**********500***********/

        /**********400***********/

        /**********300***********/

        /**********200***********/

        /**********100***********/

        /**********90************/

        /**********80************/

        /**********70************/

        /**********60************/

        /**********50************/
        BlackHole_Store = 50, Volcano_Store, Slowdown_Store,
        /**********40************/

        /**********30************/

        /**********20************/

        /**********10************/

    }

    public enum SkillPrice
    {
        //등급 별로 가격나눔 임시로 원,투,쓰리
        One = 150, Two = 250, Three = 400
    }
}
