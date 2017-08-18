//
//  main.cpp
//  design-pattern
//
//  Created by Ernest on 5/10/17.
//  Copyright © 2017 Ernest. All rights reserved.
//

#include "Mediator.hpp"
#include "SimpleFactory.hpp"
#include "Strategy.hpp"
#include "Template.hpp"
#include "Facade.hpp"
#include "Singleton.hpp"
using namespace mediator;
using namespace simple_factory;
using namespace strategy;
using namespace template_;
using namespace facade;
using namespace singleton;

int main(int argc, const char * argv[]) {
    //MediatorTest();
    //SimpleFactoryTest();
    //StrategyTest();
    //TemplateTest();
    //FacadeTest();
    SingletonTest();
    return 0;
}
