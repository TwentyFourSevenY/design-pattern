//
//  Template.hpp
//  design-pattern
//
//  Created by Ernest on 5/10/17.
//  Copyright © 2017 Ernest. All rights reserved.
//

#ifndef Template_hpp
#define Template_hpp

#include <iostream>
#include <string>

//利用Strategy類別為例子，將Manufacture的共同點抽取到base class
namespace template_ {
    using namespace std;
    class Car;
    class StrategyContext{
        Car *car;
    public:
        StrategyContext(Car* car){
            this->car = car;
        }
        string Manu();
    };
    
    class Car{
    public:
        virtual string Manufacture() = 0;
    };
    
    class FordCar : public Car{
    public:
        FordCar():Car(){};
        string Manufacture();
    };
    
    class MitsubishiCar : public Car{
    public:
        string Manufacture();
    };
    
    class ToyotaCar : public Car{
        string Manufacture();
    };
    
    void TemplateTest();
}

#endif /* Template_hpp */
