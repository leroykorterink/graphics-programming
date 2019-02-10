class StaticComponent {
  constructor() {
    if (new.target === StaticComponent)
      throw new Error("Don't create new instanc of `basic`!!!");
  }
}

export default StaticComponent;
