import StaticComponent from "../core/StaticComponent.js";
import Trees from "./Forest.js";

const getRandomClamped = (min = 0.7, max = 1) =>
  Math.random() * (max - min) + min;

class Tree extends StaticComponent {
  constructor(scene) {
    super();

    const leaves = this.createLeaves();
    const trunk = this.createTrunk();

    const tree = new THREE.Group();
    tree.add(trunk);
    tree.add(leaves);

    scene.add(tree);
  }

  createTrunk() {
    const geometry = new THREE.CylinderGeometry(0.25, 0.25, 6, 6, 2, true);
    const material = new THREE.MeshLambertMaterial({
      color: "#382C21"
    });

    const mesh = new THREE.Mesh(geometry, material);

    mesh.position.y += 3;
    mesh.castShadow = true; //default is false

    return mesh;
  }

  createLeaves() {
    const geometry = new THREE.IcosahedronGeometry(4, 0);
    const material = new THREE.MeshLambertMaterial({
      color: "#008040"
    });

    geometry.scale(
      getRandomClamped(0.6, 1),
      getRandomClamped(0.3, 0.6),
      getRandomClamped(0.6, 1)
    );

    const mesh = new THREE.Mesh(geometry, material);

    mesh.setRotationFromEuler(
      new THREE.Euler(getRandomClamped(0, 1), 0, getRandomClamped(0, 1))
    );

    mesh.position.y += 6;
    mesh.castShadow = true; //default is false

    return mesh;
  }
}

export default Tree;
