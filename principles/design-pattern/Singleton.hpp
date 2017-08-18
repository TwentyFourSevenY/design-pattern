//
//  Singleton.hpp
//  design-pattern
//
//  Created by Ernest on 5/10/17.
//  Copyright © 2017 Ernest. All rights reserved.
//

#ifndef Singleton_hpp
#define Singleton_hpp

#include <iostream>
#include <string>

namespace singleton {
    using namespace std;
    class Ramen {
    public:
        static Ramen* ManuRaman();
    private:
        Ramen(){}
        static int count;
    };
    
    void SingletonTest();
}

#endif /* Singleton_hpp */
