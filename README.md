<h1 align="center">BreadGPT</h1>

<p align="center">
  <img src="https://img.shields.io/badge/Language-C%23-239120?style=flat&logo=c-sharp&logoColor=white"/>
  <img src="https://img.shields.io/badge/Framework-WPF-0078D7?style=flat&logo=microsoft&logoColor=white"/>
  <img src="https://img.shields.io/badge/Database-SQLite-003B57?style=flat&logo=sqlite&logoColor=white"/>
  <img src="https://img.shields.io/badge/Model-Mistral_AI-1E88E5?style=flat&logo=ai&logoColor=white"/>
</p>

## Основной функционал

- **Отправка запросов**: Пользователи могут отправлять запросы к нейросети Mistral AI и получать ответы.
- **Создание чатов**: Возможность вести несколько чатов одновременно.
- **Сохранение истории**: Все данные чатов и запросов сохраняются в локальной базе данных SQLite.
- **Планируемые функции**: 
  - Добавление памяти и контекста для улучшения взаимодействия с нейросетью.

## Структура проекта

- `Models/` — Модели данных.
- `ViewModels/` — Реализация логики приложения с использованием CommunityToolkit.MVVM.
- `Views/` — Интерфейс пользователя (WPF).
- `Data/` — Скрипты и структура SQLite базы данных.
- `Services/` — Сервисы для работы с API Mistral AI и базой данных.
- `Components/` — UI Компоненты.

## Установка и запуск

Клонируйте репозиторий:
 
```bash
git clone https://github.com/Bre4dGC/BreadGPT.git
