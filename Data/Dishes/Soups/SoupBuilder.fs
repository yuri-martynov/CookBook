namespace Dishes

open Types
open Products
open RecipeBuilder

type SoupBuilder() = 
    inherit RecipeBuilder()

    let мясной_бульон = 
        [ шаг {
            шаг     "подготовить продукты для мясного бульона"
            время   2 мин
            состав  вода  2 л
            состав  мясо  0.5 кг
            состав  соль  1 ст_л
            состав  перец_горошек 4 шт
            состав  лук 1 шт    
          } 
        ; шаг 
          {
            шаг     "Сварить мясной бульон"
            время   1 ч
          } 
        ; шаг 
          {
            шаг     "Процедить мясной бульон"
            время  1 мин
          }
        ]

    let лук x u = шаг {
        шаг     "Почистить лук, нарезать"
        время   3 мин
        состав  лук   x u
    }

    let свекла = шаг {
        шаг     "Очистить, нарезать соломкой свёклу"
        время  3 мин
        состав  свекла    1 шт    
    }    
    
    let морковь = шаг {
        шаг     "Очистить, натереть на крупной терке мокровь"
        время   3 мин
        состав  морковь   1 шт 
    }
    
    let добавить_капусту x u = шаг {
        шаг     "Нарезать капусту, добавить в бульон"
        время   2 мин
        состав  капуста   x u
    }
    
    let нарезать_помидоры = шаг {
        шаг     "Очистить от кожуры, нарезать помидоры"
        время   3 мин
        состав  томат   2 шт        
    }

    [<CustomOperation("мясной бульон")>] member this.MeatBoulion(dish) = this.Step(dish, мясной_бульон)
    [<CustomOperation("свеклу нарезать соломкой")>] member this.Beet(dish) = this.Step(dish, свекла)
    [<CustomOperation("морковь натереть на крупной терке")>] member this.Carrot(dish) = this.Step(dish, морковь)
    [<CustomOperation("капусту нарезать и добавить в бульон")>] member this.Cabbage(dish, x, u) = this.Step(dish, добавить_капусту x u)
    [<CustomOperation("помидоры очистить от кожуры и нарезать")>] member this.Tomatoes(dish) = this.Step(dish, нарезать_помидоры)
    [<CustomOperation("лук почистить и нарезать")>] member this.Onioin(dish, x, u) = this.Step(dish, лук x u)
    [<CustomOperation("варить")>] member this.Boil(dish, t, u) = this.Process(dish, "варить", t, u)
        

 module RecipeBuilder =
     let суп = SoupBuilder()
        
    
