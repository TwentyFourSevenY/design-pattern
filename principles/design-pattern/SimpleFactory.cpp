//
//  SimpleFactory.cpp
//  design-pattern
//
//  Created by Ernest on 5/12/17.
//  Copyright © 2017 Ernest. All rights reserved.
//

#include "SimpleFactory.hpp"

using namespace simple_factory;

Car* CarFactory::CreateCar(car_type ty){
    Car *car;
    switch (ty) {
        case ford:
            car = new FordCar();
            break;
        case mitsubishi:
            car = new MitsubishiCar();
            break;
        case toyota:
            car = new ToyotaCar();
            break;
        default:
            break;
    }
    return car;
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

void simple_factory::SimpleFactoryTest(){
    bool noError = true;
    
    Car *car;
    
    car = CarFactory::CreateCar(CarFactory::ford);
    noError &= car->Manufacture() == "製造了Ford的車子";
    
    car = CarFactory::CreateCar(CarFactory::mitsubishi);
    noError &= car->Manufacture() == "製造了Mitsubishi的車子";
    
    car = CarFactory::CreateCar(CarFactory::toyota);
    noError &= car->Manufacture() == "製造了Toyota的車子";
    
    if (noError)
        cout<<"測試成功"<<endl;
    else
        cout<<"測試失敗"<<endl;
}
