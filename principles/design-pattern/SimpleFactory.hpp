//
//  SimpleFactory.hpp
//  design-pattern
//
//  Created by Ernest on 5/12/17.
//  Copyright © 2017 Ernest. All rights reserved.
//

#ifndef SimpleFactory_hpp
#define SimpleFactory_hpp

#include <iostream>
#include <string>
namespace simple_factory {
    using namespace std;
    class Car;
    class CarFactory{
    public:
        enum car_type {ford, mitsubishi, toyota};
        static Car* CreateCar(car_type);
    };
    
    class Car{
    public:
        virtual string Manufacture() = 0;
    };
  
    class FordCar : public Car{
    public:
        string Manufacture();
    };
    
    class MitsubishiCar : public Car{
    public:
        string Manufacture();
    };
    
    class ToyotaCar : public Car{
        string Manufacture();
    };
    
    void SimpleFactoryTest();
}

#endif /* SimpleFactory_hpp */
