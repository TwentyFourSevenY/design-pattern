//
//  Singleton.cpp
//  design-pattern
//
//  Created by Ernest on 5/10/17.
//  Copyright © 2017 Ernest. All rights reserved.
//

#include "Singleton.hpp"

using namespace singleton;

int Ramen::count = 0;

Ramen* Ramen::ManuRaman(){
    count++;
    if (count <= 2)
        return new Ramen();
    else
        return nullptr;
}

void singleton::SingletonTest(){
    bool noError = true;
    int bowlNumber = 0;
    
    Ramen *ramen1 = Ramen::ManuRaman();
    if (ramen1!=nullptr) bowlNumber++;
    Ramen *ramen2 = Ramen::ManuRaman();
    if (ramen2!=nullptr) bowlNumber++;
    Ramen *ramen3 = Ramen::ManuRaman();
    if (ramen3!=nullptr) bowlNumber++;
    
    if (bowlNumber>=3)
        noError = false;
    
    if (noError)
        cout<<"測試成功"<<endl;
    else
        cout<<"測試失敗"<<endl;
}
