//
//  Mediator.cpp
//  design-pattern
//
//  Created by Ernest on 5/10/17.
//  Copyright © 2017 Ernest. All rights reserved.
//

#include "Mediator.hpp"
using namespace mediator;

//Mediator
void Calculator::Process(Keys *key){
    if (!pressedNumber)
        keyResult->Reset();
    if (dynamic_cast<Key1*>(key)) {
        keyResult->SetNumber(key1->GetNumber());
        pressedNumber = true;
    } else if (dynamic_cast<Key2*>(key)){
        keyResult->SetNumber(key2->GetNumber());
        pressedNumber = true;
    } else if (dynamic_cast<KeyAdd*>(key)){
        if (!pressedNumber)
            return;
        Proc();
        keyAdd->SetNumber(keyResult->GetNumber());
        lastOp = key;
        pressedNumber = false;
    } else if (dynamic_cast<KeyMul*>(key)){
        if (!pressedNumber)
            return;
        Proc();
        keyMul->SetNumber(keyResult->GetNumber());
        lastOp = key;
        pressedNumber = false;
    } else if (dynamic_cast<KeyResult*>(key)){
        Proc();
        cout<<keyResult->GetNumber()<<endl;
        pressedNumber = false;
    } else{
        cout<<"error"<<endl;
    }
}
void Calculator::Proc(){
    if (lastOp) {
        lastOp->SetNumber(keyResult->GetNumber());
        keyResult->Reset();
        keyResult->SetNumber(lastOp->GetNumber());
        lastOp->Reset();
    }
}
void Calculator::SetKey(Keys *key){
    if (dynamic_cast<Key1*>(key)){
        this->key1 = static_cast<Key1*>(key);
    } else if (dynamic_cast<Key2*>(key)) {
        this->key2 = static_cast<Key2*>(key);
    } else if (dynamic_cast<KeyAdd*>(key)) {
        this->keyAdd = static_cast<KeyAdd*>(key);
    } else if (dynamic_cast<KeyMul*>(key)) {
        this->keyMul = static_cast<KeyMul*>(key);
    } else if (dynamic_cast<KeyResult*>(key)) {
        this->keyResult = static_cast<KeyResult*>(key);
    }
}

//Member
int Keys::GetNumber(){
    return value;
}

void Key1::Press(){
    _cal->Process(this);
}

void Key2::Press(){
    _cal->Process(this);
}

void KeyAdd::Press(){
    _cal->Process(this);
}
void KeyAdd::SetNumber(int i){
    value += i;
}
void KeyAdd::Reset(){
    value = 0;
}

void KeyMul::Press(){
    _cal->Process(this);
}
void KeyMul::SetNumber(int i){
    value *= i;
}
void KeyMul::Reset(){
    value = 1;
}

void KeyResult::Press(){
    _cal->Process(this);
}
void KeyResult::SetNumber(int i){
    value = value * 10 + i;
}
void KeyResult::Reset(){
    value = 0;
}

void mediator::MediatorTest(){
    Calculators *cal = new Calculator();
    Keys *key1 = new Key1(cal);
    Keys *key2 = new Key2(cal);
    Keys *keyAdd = new KeyAdd(cal);
    Keys *keyMul = new KeyMul(cal);
    Keys *keyResult = new KeyResult(cal);
    cal->SetKey(key1);
    cal->SetKey(key2);
    cal->SetKey(keyAdd);
    cal->SetKey(keyMul);
    cal->SetKey(keyResult);
    
    //test
    key1->Press();
    key1->Press();
    keyAdd->Press();
    keyAdd->Press();
    key2->Press();
    keyMul->Press();
    keyMul->Press();
    keyMul->Press();
    key2->Press();
    keyAdd->Press();
    keyAdd->Press();
    key2->Press();
    keyMul->Press();
    key2->Press();
    key1->Press();
    
    keyResult->Press();
}
