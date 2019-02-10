import AdvancedComponent from "./AdvancedComponent.js";

class Scene {
  constructor(components) {
    // Save components for later reference
    this.world = new THREE.Scene();

    // Add all components to scene
    this.components = components.reduce(
      (accumulator, componentType) => [
        ...accumulator,
        new componentType(this.getScene())
      ],
      []
    );
  }

  addComponent(component) {
    this.components.push(new component(this.getScene()));
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
