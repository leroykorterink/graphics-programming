import AdvancedComponent from "../core/AdvancedComponent.js";

class Box extends AdvancedComponent {
  constructor() {
    super();

    var geometry = new THREE.BoxGeometry(1, 1, 1);
    var material = new THREE.MeshNormalMaterial({ color: 0x00ff00 });

    this.velocity = -0.005;
    this.object = new THREE.Mesh(geometry, material);
  }

  update() {
    if (this.velocity >= 0 && this.object.position.x > 2) {
      this.velocity = -0.005;
    }

    if (this.velocity < 0 && this.object.position.x < -2) {
      this.velocity = 0.005;
    }

    this.object.position.set(
      this.object.position.x + this.velocity,
      this.object.position.y,
      0
    );
  }
}

export default Box;
