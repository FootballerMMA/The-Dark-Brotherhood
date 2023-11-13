using System;
using static JitsuCard;
class JitsuElementSet {
    private JitsuCard[] elementSet = new JitsuCard[15];
    public JitsuElementSet (int element){
        int x = 0;
        int i = 0;
        for (i = 2; i < 8; i++, x+=2){
            JitsuCard card = new JitsuCard(element, i);
            elementSet[x] = card;
            JitsuCard card2 = new JitsuCard(element, i);
            elementSet[x + 1] = card2;
        }
        for (i = 1; i <= 3; i++, x++){
            JitsuCard card = new JitsuCard(element, i + 7);
            elementSet[x] = card;
        }
    }
    public JitsuCard[] getSet(){
        return elementSet;
    }
}