import StaticComponent from "../core/StaticComponent.js";
import makeObjLoader from "../util/makeObjLoader.js";
import makeLoadTexture from "../util/makeLoadTexture.js";

const loadOBJ = makeObjLoader(fileName => `assets/Fence/${fileName}`);
const loadTexture = makeLoadTexture(fileName => `assets/Fence/${fileName}`);


export default (position, angle) =>
  class Fence extends StaticComponent {
    constructor(scene) {
      super();

      this.init(scene);
    }

    async init(scene) {
      const mesh = (await loadOBJ("fence.obj")).children[0];
      const material = await loadTexture("texture.png");

      mesh.material.map = material;
      // // Scale down and move to right position
      mesh.applyMatrix(new THREE.Matrix4().makeScale(0.1, 0.1, 0.1));

      mesh.geometry.computeBoundingBox();

      const x =  Math.abs(mesh.geometry.boundingBox.min.x - mesh.geometry.boundingBox.max.x);

      //Rotate model 270 with Radians 
      mesh.rotateZ(3 * Math.PI / 2);

      //Because of rotation X is now Y
      mesh.translateX(x + 4.9 - position.y);

      //Because of rotation Y is now X
      mesh.translateY(4 + position.x);

      mesh.translateZ(-1 + position.z);

      scene.add(mesh);
    }
  };
