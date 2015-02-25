# Sprite-Sensors
A unity project that contains main agent, enemy agents, and at least 2 walls.  The main agent will move based on user input.  It can rotate its heading or move forward/backward.  The main agent will also contain three classes.  Each class represents a different type of sensor for the main agent to interpret other objects in its environment.

Wall Sensor: creates 3 rays extending from front of agent that detects only walls
Adjacent Agent Sensor: detects any enemy objects with a certain radius
Pie Sensor: detects any enemy objects with a certain radius and where they are in relation to main agent(front, back, left, right)

Read assignment_requirements.pdf to understand the entire project and what it does in detail.  Read code summary to get detailed explanations of how the code in the sensor classes works.  Look at screenshots folder to see examples of sensors working in real time.
