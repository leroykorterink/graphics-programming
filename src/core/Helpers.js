class Helpers {
  constructor(renderer) {
    this.toggleDebug = this.toggleDebug.bind(this);

    this.scene = renderer.scene;
    this.camera = renderer.camera;

    this.debug = false;

    this.helpers = [
      new THREE.AxesHelper(5),
      new THREE.GridHelper(400, 400, 0x0000ff, 0x808080)
    ];

    document
      .querySelector("#toggleDebug")
      .addEventListener("click", this.toggleDebug);

    this.toggleDebug();
  }

  toggleDebug() {
    this.debug = !this.debug;
    const scene = this.scene.getScene();

    if (this.debug) scene.add(...this.helpers);
    else scene.remove(...this.helpers);
  }
}

export default Helpers;
