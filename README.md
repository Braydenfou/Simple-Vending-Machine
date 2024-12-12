# Simple Vending Machine

Welcome to my Simple Vending Machine project! This application simulates a vending machine that allows users to select items, view their total cost, and process payments.

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Development Approach](#development-approach)
- [Object-Oriented Design](#object-oriented-design)
- [Future Work](#future-work)
- [License](#license)

## Overview

The Simple Vending Machine application showcases a selection of 12 items, each with an associated price. Users can interact with the vending machine to add items to their cart, clear their selections, and complete transactions via cash or card. 

Upon successful payment, the application displays a receipt summarizing the purchase.

## Features

- **Item Selection**: Add items to a virtual cart by clicking item buttons.
- **Clear Button**: Reset the cart, removing all selected items and recalculating the total cost.
- **Checkout Button**: Process payments via cash or card and display a receipt.
- **Receipt Page**: Summarizes items purchased, total cost, payment method, and additional details like change for cash payments.

## Development Approach

The project was developed using a structured algorithmic thinking process:

1. **Understanding the Problem**: Defined requirements for item selection, payment handling, and receipt generation.
2. **Formulating the Problem**: Designed classes and components to handle vending machine operations.
3. **Developing the Algorithm**: Created pseudocode and flowcharts to map out program logic.
4. **Implementing the Algorithm**: Wrote code incrementally, utilizing GitHub for version control.
5. **Testing**: Conducted thorough testing to debug and optimize the application.

## Object-Oriented Design

The application leverages object-oriented programming principles to ensure modularity and maintainability:

- **Item Class**: Represents individual vending machine items, storing their name, price, and quantity.
- **Vending Machine Class**: Manages item inventory, selection, and pricing logic.
- **Pricing Class**: Handles cost calculations and updates the total dynamically.

### Main Window

Displays the 12 available items as clickable buttons. Includes a cart section to view selected items and their total cost, as well as checkout and clear buttons.

### Checkout Window

Prompts the user to choose a payment method (cash or card). Validates card payments and handles cash payments by calculating change and presenting a detailed breakdown.

### Receipt Page

Generates a receipt with:
- Purchased items and their prices
- Total cost
- Payment method
- For cash payments: selected bill amount, change due, and bill breakdown
- For card payments: a thank-you message

## Future Work

Planned improvements to enhance functionality:
- Implement stock management for items and disable out-of-stock buttons.
- Expand pricing logic to support discounts and validations.
- Enable saving receipts to a file for transaction history.

**Update**: All planned features have been implemented.

## License

This project is licensed under the MIT License. See the `LICENSE` file for more details.
