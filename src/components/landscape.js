import StaticComponent from "../core/StaticComponent.js";

class Landscape extends StaticComponent {
  constructor(scene) {
    super();

    const geometry = new THREE.PlaneGeometry(50, 50, 30);
    const material = new THREE.MeshBasicMaterial({
      color: 0xffff00,
      side: THREE.DoubleSide
    });

    this.object.rotation.x = Math.PI / 2;

    scene.add(new THREE.Mesh(geometry, material));
  }
}

export default Landscape;
