//
//  Template.cpp
//  design-pattern
//
//  Created by Ernest on 5/10/17.
//  Copyright © 2017 Ernest. All rights reserved.
//

#include "Template.hpp"

using namespace template_;

string StrategyContext::Manu(){
    //提取"製造了"與"的車子"兩共同點至此base class
    return "製造了"+this->car->Manufacture()+"的車子";
}

string FordCar::Manufacture(){
    return "Ford";
}

string MitsubishiCar::Manufacture(){
    return "Mitsubishi";
}

string ToyotaCar::Manufacture(){
    return "Toyota";
}

void template_::TemplateTest(){
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
