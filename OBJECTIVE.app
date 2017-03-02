//  This program generate 2 Matrix and then Multiply them
import Foundation
import CoreFoundation
enum MatrixError: Error {
    case invalidDimensionSize
    case lessThanZero
}
protocol Requirements {
    var Columns : Int { get }
    var Rows : Int { get }
    func Multiply(A: Matrix, B: Matrix) throws
    func Display()
}
class Matrix : Requirements {
    static let SAmount : Int = 5
	let Columns : Int
    let Rows : Int
    private var Value = [[Int]]()
    init(Rows: Int, Columns: Int) throws {
        guard Columns > 0 && Rows > 0 else {
            throw MatrixError.lessThanZero
        }
        self.Rows=Rows
        self.Columns=Columns
        for _ in 0..<Rows {
            var subArray = [Int]()
            for _ in 0..<Columns {
                let Temp = rand()%100
                subArray.append(Int(Temp))
            }
            Value.append(subArray)
        }
    }
    subscript(row: Int, column: Int) -> Int {
        get {
            return Value[row][column]
        }
        set {
            Value[row][column] = newValue
        }
    }
    func Multiply(A: Matrix, B: Matrix) throws {
        guard A.Columns == B.Rows else {
                throw MatrixError.invalidDimensionSize
        }
        for i in 0..<A.Rows {
            for j in 0..<B.Columns {
            var Amount = 0
                for k in 0..<A.Columns {
                    Amount += A[i, k] * B[k, j]
                    }
                self[i, j] = Amount
                }
            }
        }
    func Display(){
		print("Matrix of size \(Rows):\(Columns).")
        for i in 0..<self.Rows {
            for j in 0..<self.Columns {
                print(self[i, j], terminator: "\t")
            }
            print()
        }
    }
	deinit{
			print("Matrix of size \(Rows):\(Columns) has been deleted.")
		}
	func Increase(Amount: Int){
		for i in 0..<self.Rows {
            for j in 0..<self.Columns {
				self[i, j] *= Amount
			}
		}
		print("Values has been multiplied by \(Amount).")
		self.Display()
	}
	func Decrease(Amount: Int){
		for i in 0..<self.Rows {
            for j in 0..<self.Columns {
				self[i, j] /= Amount
			}
		}
		print("Values has been divided by \(Amount).")
		self.Display()
	}
	static func Randomizer(Matrix: Matrix){
		for i in 0..<Matrix.Rows {
            for j in 0..<Matrix.Columns {
				let Temp = rand()%100
				Matrix[i, j] = Int(Temp)
			}
		}
		print("Values of matrix of size \(Matrix.Rows):\(Matrix.Columns) has been randomized.")
	}
}
class MatrixUpgraded:Matrix {
	override func Increase(Amount: Int){
		for i in 0..<self.Rows {
            for j in 0..<self.Columns {
				self[i, j] += Amount
			}
		}
		print("Values has been increased by \(Amount).")
		self.Display()
	}
	func Increase(Amount: Int, Multiplier: Int){
		for i in 0..<self.Rows {
            for j in 0..<self.Columns {
				self[i, j] = (self[i, j] + Amount) * Multiplier
			}
		}
		print("Values has been increased by \(Amount) , and then multiplied by \(Multiplier).")
		self.Display()
	}
	override func Decrease(Amount: Int){
		for i in 0..<self.Rows {
            for j in 0..<self.Columns {
				self[i, j] -= Amount
			}
		}
		print("Values has been decreased by \(Amount).")
		self.Display()
	}
}
let time = UInt32(NSDate().timeIntervalSinceReferenceDate)
srand(time)
var Matrix1Rows = 20
var Matrix1Columns = 3
var Matrix2Rows = 3
var Matrix2Columns = 14
do{
    var Matrix1 = try Matrix(Rows: Matrix1Rows, Columns: Matrix1Columns)
    Matrix1.Display()
	Matrix.Randomizer(Matrix: Matrix1)
	Matrix1.Display()
	Matrix1.Increase(Amount: Matrix.SAmount)
	Matrix1.Decrease(Amount: 10)
    var Matrix2 = try MatrixUpgraded(Rows: Matrix2Rows, Columns: Matrix2Columns)
    Matrix2.Display()
	Matrix2.Increase(Amount: Matrix.SAmount)
	Matrix2.Decrease(Amount: 7)
	Matrix2.Increase(Amount: 20,Multiplier: Matrix.SAmount)
    var FinalMatrix = try MatrixUpgraded(Rows: Matrix1.Rows, Columns: Matrix2.Columns)
    try FinalMatrix.Multiply(A: Matrix1,B: Matrix2)
    FinalMatrix.Display()
	} 
    catch MatrixError.invalidDimensionSize {
        print("Matrix size is wrong for Multiplication")
        }
    catch MatrixError.lessThanZero where (Matrix1Rows > 0) && (Matrix1Columns > 0) {
        print("Second Matrix size is less than zero")    
        }
    catch MatrixError.lessThanZero {
        print("First Matrix size is less than zero")
    }