import StaticComponent from "../core/StaticComponent.js";

class Landscape extends StaticComponent {
  constructor() {
    super();

    const geometry = new THREE.PlaneGeometry(50, 50, 30);
    const material = new THREE.MeshBasicMaterial({
      color: 0xffff00,
      side: THREE.DoubleSide
    });

    const plane = new THREE.Mesh(geometry, material);

    plane.rotation.x = Math.PI / 2;
  }
}

export default Landscape;
