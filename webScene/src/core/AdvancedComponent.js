import StaticComponent from "./StaticComponent.js";

class AdvancedComponent extends StaticComponent {
  constructor() {
    if (new.target === AdvancedComponent)
      throw new Error("Don't create new instanc of `basic`!!!");

    super();
  }

  update() {
    throw new Error(`Update method for component ${this.name} not defined`);
  }
}

export default AdvancedComponent;
