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