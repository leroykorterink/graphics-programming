import StaticComponent from "../core/StaticComponent.js";

const SIZE = 1000;

const directions = ["front", "back", "top", "bottom", "right", "left"];
const createPath = direction => `assets/images/cloudtop_${direction}.png`;

class Skybox extends StaticComponent {
  constructor(scene) {
    super();

    const materials = directions.map(
      direction =>
        new THREE.MeshBasicMaterial({
          map: THREE.ImageUtils.loadTexture(createPath(direction)),
          side: THREE.BackSide
        })
    );

    const geometry = new THREE.CubeGeometry(SIZE, SIZE, SIZE);
    const material = new THREE.MeshFaceMaterial(materials);

    scene.add(new THREE.Mesh(geometry, material));
  }
}

export default Skybox;
