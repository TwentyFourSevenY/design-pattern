//
//  Strategy.cpp
//  design-pattern
//
//  Created by Ernest on 5/10/17.
//  Copyright © 2017 Ernest. All rights reserved.
//

#include "Strategy.hpp"

using namespace strategy;

string StrategyContext::Manu(){
    return this->car->Manufacture();
}

string FordCar::Manufacture(){
    return "製造了Ford的車子";
}

string MitsubishiCar::Manufacture(){
    return "製造了Mitsubishi的車子";
}

string ToyotaCar::Manufacture(){
    return "製造了Toyota的車子";
}

void strategy::StrategyTest(){
    bool noError = true;
    StrategyContext* car;
    
    car = new StrategyContext(new FordCar());
    noError &= car->Manu() == "製造了Ford的車子";
    
    car = new StrategyContext(new MitsubishiCar());
    noError &= car->Manu() == "製造了Mitsubishi的車子";
    
    car = new StrategyContext(new ToyotaCar());
    noError &= car->Manu() == "製造了Toyota的車子";
    
    if (noError)
        cout<<"測試成功"<<endl;
    else
        cout<<"測試失敗"<<endl;
}
