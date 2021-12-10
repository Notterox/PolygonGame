# Игра многоугольников
Тестовое задание для Dekovir

## Основные термины
* `Creature` - существо, житель, фигура
* `StickCreature` - существо с двумя вершинами
* `RegularCreature` - существо-правильный многоугольник

## Примечания по исходникам

* `Assets/CodeBase/Creature/Game.cs` - класс игры, входная точка
* `Assets/CodeBase/Creature/Creature.cs` - класс с логикой существа
* `Assets/CodeBase/Creature/Behavior/` - директория с поведением существа
* `Assets/CodeBase/Creature/{StickCreatureDirections, RegularCreatureDirections}.cs` - реализации провайдера доступных направлений движения существа
* `Assets/CodeBase/Services/MeshGeneratingService` - сервис генерации мешей для многоугольников
* `Assets/Resources/StaticData/Defaults.asset` - ассет со значениями по-умолчанию

## Примечание по реализации
* все взаимодействие между существами построено на встроенном в Unity физическом движке
* нахождение целей для агрессивного поведения реализовано наивным переборов всех существ
* спавн существ использует object pool
## P.S.
* синие существа могут показаться "застрявшими" у границы, но это нормально, они пытаются выбрать случайное направление, согласно ТЗ
* выход из игры через `ALT + F4` :)
## Скриншоты
![image](https://user-images.githubusercontent.com/12456395/145509093-d4a9082e-75d1-42d0-b4f8-21a7551c286e.png)
![image](https://user-images.githubusercontent.com/12456395/145509245-4cd13baf-41da-44be-b9b5-14c017aa337c.png)
