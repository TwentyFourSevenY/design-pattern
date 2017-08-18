//
//  Facade.cpp
//  design-pattern
//
//  Created by Ernest on 5/10/17.
//  Copyright © 2017 Ernest. All rights reserved.
//

#include "Facade.hpp"

using namespace facade;

bool Travel::PurchaseTour(){
    return hr->Booking() && cr->Rental();
}

bool HotelRentaler::Booking(){
    return true;
}

bool CarRentaler::Rental(){
    return true;
}

void facade::FacadeTest(){
    bool noError = true;
    
    Travel *travel = new Travel();
    
    noError &= travel->PurchaseTour();
    
    if (noError)
        cout<<"測試成功"<<endl;
    else
        cout<<"測試失敗"<<endl;
}
