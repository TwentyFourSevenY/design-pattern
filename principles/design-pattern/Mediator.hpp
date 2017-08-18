//
//  Mediator.hpp
//  design-pattern
//
//  Created by Ernest on 5/10/17.
//  Copyright © 2017 Ernest. All rights reserved.
//

#ifndef Mediator_hpp
#define Mediator_hpp

#include <iostream>
#include <string>
namespace mediator{
    using namespace std;
    
    //以下採計算機為例，各個按鍵點擊後不應該知道數值顯示在哪裡......等，需要進行解耦
    
    //Mediator
    class Calculators;  //僅稍抽出介面，並未考慮實際擴充的概況
    class Calculator;
    //Member
    class Keys;         //按鍵的介面，依按鍵而言都有點擊及其計算數值的功能
    class Key1;
    class Key2;
    class KeyAdd;
    class KeyMul;
    class KeyResult;
    
    //Mediator
    class Calculators
    {
    public:
        Calculators(){}
        virtual void Process(Keys*) = 0;
        virtual void SetKey(Keys*) = 0;
    private:
        virtual void Proc() = 0;
    };
    
    //ConcreteMediator
    class Calculator : public Calculators
    {
        Key1 *key1;
        Key2 *key2;
        KeyAdd *keyAdd;
        KeyMul *keyMul;
        KeyResult *keyResult;
        Keys *lastOp;
        bool pressedNumber;
    public:
        Calculator() : Calculators(){
            lastOp = nullptr;
            pressedNumber = false;
        }
        void Process(Keys*);
        void SetKey(Keys *key);
    private:
        void Proc();
    };

    
    //Member
    class Keys
    {
    protected:
        int value;
        Calculators *_cal;
    public:
        Keys(Calculators *cal){
            _cal = cal;
        }
        virtual void Press() = 0;
        int GetNumber();
        virtual void SetNumber(int){};
        virtual void Reset(){};
    };
    
    //ConcreteMember
    class Key1 : public Keys
    {
    public:
        Key1(Calculators *cal) : Keys(cal){
            value = 1;
        }
        void Press();
    };
    
    class Key2 : public Keys
    {
    public:
        Key2(Calculators *cal) : Keys(cal){
            value = 2;
        }
        void Press();
    };
    
    class KeyAdd : public Keys
    {
    public:
        KeyAdd(Calculators *cal) : Keys(cal){
            value = 0;
        }
        void Press();
        void SetNumber(int);
        void Reset();
    };
    
    class KeyMul : public Keys
    {
    public:
        KeyMul(Calculators *cal) : Keys(cal){
            value = 1;
        }
        void Press();
        void SetNumber(int);
        void Reset();
    };
    
    class KeyResult : public Keys
    {
    public:
        KeyResult(Calculators *cal) : Keys(cal){
            value = 0;
        }
        void Press();
        void SetNumber(int);
        void Reset();
    };
    
    void MediatorTest();
}

#endif /* Mediator_hpp */
