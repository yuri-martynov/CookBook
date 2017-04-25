﻿namespace Dishes

open Products
open RecipeBuilder

[<AutoOpen>]
module Soups2 =

    let щи = суп {
        блюдо   "щи из свежей капусты"

        ``мясной бульон``
        ``морковь натереть на крупной терке``
        

        
        шаг     "обжарить морковь с луком"
        время   5 мин
        
        шаг     "добавить поджаренные овощи в бульон"

        ``капусту нарезать и добавить в бульон`` 400 г
        варить  20 мин
        
        ``помидоры очистить от кожуры и нарезать``

        шаг     "добавить помидоры в бульон"
        варить   3 мин
        
        шаг     "добавить нарезанную зелель"
        состав  зелень    1 шт

        по_вкусу перец
    }
