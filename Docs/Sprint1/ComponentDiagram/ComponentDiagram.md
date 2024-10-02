# Component Diagram C4

In the first image, we can visualize the component diagram up to the third level. At that point, we can deeply analyze all three levels.

## **NV1 & NV2**
On level 1, we see the application as a black box.

On level 2, we have three important components

- **BackEnd**

    - Component that communicates via API with the FrontEnd, Planning and the External Facing API

- **FrontEnd**

    - Component that communicates via API with the BackEnd and the Front Facing UI

- **Planning**

    - Component that only communicates via API with the BackEnd,, sending and receiving messages

![CD_1_2_3](NV1_2/png/ComponentDiagram1_2.png "Component Diagram")

## **NV3**
When we analyze level 3 inside the BackEnd, we can see some relevant components.

- **Server**

    - Component that handles all the interactions via API mentioned before and consumes the Database via API

- **Database**

    - Component that returns information via API to the Server



![CD_1_2_3](NV3/png/ComponentDiagram_LV3.png "Component Diagram")

## **NV4**

When we reach the fourth level inside the Server, we see many components.

- **Controller**

    - Responsible for communicating with the FrontEnd via API
    - Asks for information from Services

- **Services**

    - Requests information from the repositories
    - Manages the information with the Model
    - Requests information from the Planning Interface about the planning

- **Model**

    - Handles the business logic
    - Interacts with Services, providing information about the business logic

- **Repositories**

    - Persists information by sending it to the Database via API

- **Repositories Interface**

    - Interacts with Services to abstract the repositories, making it possible to have different database types (e.g., relational, non-relational databases) without needing to modify our business logic

- **Planning Interface**

    - Responsible for interacting with the Planning component, sending and receiving requests via API


![CD_4](NV4/png/ComponentDiagram_L4.png "Component Diagram 4")