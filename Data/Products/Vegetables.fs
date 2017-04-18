namespace Products

open Types
open Operators

[<AutoOpen>]
module GlobalVegetables =
    let свекла' = Whole "свекла"
    let свекла items = свекла' $ Items items    
    
    let лук' = Whole "лук"
    let лук items = лук' $ Items items

    let морковь' = Whole "морковь"
    let морковь items = морковь' $ Items items

