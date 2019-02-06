import AdvancedComponent from "./AdvancedComponent.js";

class Scene {
  constructor(components) {
    // Save components for later reference
    this.components = components || [];

    this.world = new THREE.Scene();

    // Add all components to scene
    this.world.add(...components.map(component => component.object));
  }

  addComponent(component) {
    this.components.push(component);
    this.world.add(component);
  }

  getScene() {
    return this.world;
  }

  update() {
    this.components.forEach(component => {
      if (!(component instanceof AdvancedComponent)) return;

      component.update();
    });
  }
}

export default Scene;
