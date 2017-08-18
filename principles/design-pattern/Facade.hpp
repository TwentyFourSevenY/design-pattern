//
//  Facade.hpp
//  design-pattern
//
//  Created by Ernest on 5/10/17.
//  Copyright © 2017 Ernest. All rights reserved.
//

#ifndef Facade_hpp
#define Facade_hpp

#include <iostream>
#include <string>
namespace facade {
    using namespace std;
    class HotelRentaler;
    class CarRentaler;
    
    class HotelRentaler{
    public:
        HotelRentaler(){}
        bool Booking();
    };
    
    class CarRentaler{
    public:
        CarRentaler(){}
        bool Rental();
    };
    
    class Travel{
        HotelRentaler *hr;
        CarRentaler *cr;
    public:
        Travel(){
            hr = new HotelRentaler();
            cr = new CarRentaler();
        }
        bool PurchaseTour();
    };
    
    void FacadeTest();
}

#endif /* Facade_hpp */
