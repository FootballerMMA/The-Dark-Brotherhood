using System;
public class JitsuCard {
    public int element; // 0 is snow 1 is fire 2 is water
    public int attackLevel;
    bool drawnToDeck;
    public JitsuCard(int element, int attackLevel){
        this.element = element;
        this.attackLevel = attackLevel;
        drawnToDeck = false;
    }
    public void setDrawnToDeck(){
        drawnToDeck = true;
    }
    public bool getDrawnToDeck(){
        return drawnToDeck;
    }
    public override string ToString(){
        string s = element + " card [" + attackLevel + "]";
        return s;
    }
}