class StaticComponent {
  constructor() {
    if (new.target === StaticComponent)
      throw new Error("Don't create new instanc of `basic`!!!");

    this.object = new THREE.Object3D();
  }
}

export default StaticComponent;
